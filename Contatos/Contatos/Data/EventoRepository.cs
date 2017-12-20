using Contatos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contatos.Data
{
    public class EventoRepository : RepositoryBase
    {
        public async Task<Evento> SelecionarAsync(int id)
        {
            // Localizar pela chave primária
            return await Conexao.FindAsync<Evento>(id);
        }

        public async Task<List<Evento>> ListarAsync()
        {
            // Listar todos os registros cadastrados
            return await Conexao.Table<Evento>().ToListAsync();
        }

        public async Task<List<Evento>> PesquisarAsync(string conteudo)
        {
            // Filtrar os registros
            return await Conexao.Table<Evento>()
                .Where(r =>
                    r.Nome.Contains(conteudo) ||
                    r.Anotacoes.Contains(conteudo))
                .ToListAsync();
        }

        public async Task<ResultadoOperacao> SalvarAsync(Evento item)
        {
            var resultado = new ResultadoOperacao()
            {
                Sucesso = true
            };

            // Executar o comando para salvar
            try
            {
                var qtd = await Conexao.InsertOrReplaceAsync(item);

                // Verificar se incluiu / alterou
                if (qtd == 0)
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = "Não foi possível salvar!";
                }
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = ex.Message;
            }

            return resultado;
        }

        public async Task<ResultadoOperacao> ExcluirAsync(Evento item)
        {
            var resultado = new ResultadoOperacao()
            {
                Sucesso = true
            };

            // Executar o comando para excluir
            try
            {
                var qtd = await Conexao.DeleteAsync(item);

                // Verificar se incluiu / alterou
                if (qtd == 0)
                {
                    resultado.Sucesso = false;
                    resultado.Mensagem = "Não foi possível excluir!";
                }
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = ex.Message;
            }

            return resultado;
        }
    }
}
