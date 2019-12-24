﻿using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, 
            IEnderecoRepository enderecoRepository,
            INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            //Validar o estado da entidade!            
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)
               || !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) return;

            //Se não existe fornecedor com o mesmo documento
            if (_fornecedorRepository.Buscar(c => c.Documento.Equals(fornecedor.Documento)).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");

                return;
            }


            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            //Se não existe fornecedor com o mesmo documento
            if (_fornecedorRepository.Buscar(c => c.Documento.Equals(fornecedor.Documento) && !c.Id.Equals(fornecedor.Id)).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento informado.");

                return;
            }

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if(!ExecutarValidacao(new EnderecoValidation(), endereco)) return;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }

        public async Task Remover(Guid id)
        {
            if (_fornecedorRepository.ObterFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notificar("O fornecedor possui produtos cadastrados.");

                return;
            }

            await _fornecedorRepository.Remover(id);
        }
    }
}
