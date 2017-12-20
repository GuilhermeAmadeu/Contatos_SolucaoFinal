using Contatos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contatos.Data
{
    public class PessoaRepository : RepositoryBase
    {
        // FindAsync = consulta uma tabela a partir de um parametro 
        public async Task<Pessoa> SelecionarAsync(int id)
        {
            // Localizar pela chave primária
            return await Conexao.FindAsync<Pessoa>(id);
        }

        // Table<modelo> = consulta todos os dados de uma tabela
        public async Task<List<Pessoa>> ListarAsync()
        {
            // Listar todos os registros cadastrados
            return await Conexao.Table<Pessoa>().ToListAsync();
        }

        // Where e Contains = consulta a partir de um dados espeficifico em uma coluna
        public async Task<List<Pessoa>> PesquisarAsync(string conteudo)
        {
            // Filtrar os registros

            var lista = Conexao.Table<Pessoa>();

            // Verificar se existe filtro
            if (!string.IsNullOrEmpty(conteudo))
            {
                lista = lista.Where(r =>
                    r.Nome.Contains(conteudo) ||
                    r.Email.Contains(conteudo) ||
                    r.Telefone.Contains(conteudo));
            }

            return await lista.ToListAsync();
        }

        // InsertOrReplaceAsync = inclui ou altera um registro na tabela do banco de dados
        public async Task<ResultadoOperacao> SalvarAsync(Pessoa item)
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
            catch(Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Mensagem = ex.Message;
            }

            return resultado;
        }


        // DeleteAsync = Exclui um registro na tabela do banco de dados
        public async Task<ResultadoOperacao> ExcluirAsync(Pessoa item)
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
