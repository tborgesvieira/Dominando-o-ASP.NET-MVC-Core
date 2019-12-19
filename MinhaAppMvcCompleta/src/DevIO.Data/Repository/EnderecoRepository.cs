using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDBContext context)
            :base(context)
        {

        }
        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await _context.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.FornecedorId.Equals(fornecedorId));
        }
    }
}
