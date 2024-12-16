using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

public class Program {
    public static void Main(string[] args) {
        Cadastro cadastro = new Cadastro();
        cadastro.Download();

        while (true) {
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Cadastrar ambiente");
            Console.WriteLine("2. Consultar ambiente");
            Console.WriteLine("3. Excluir ambiente");
            Console.WriteLine("4. Cadastrar usuario");
            Console.WriteLine("5. Consultar usuario");
            Console.WriteLine("6. Excluir usuario");
            Console.WriteLine("7. Conceder permissão de acesso ao usuario");
            Console.WriteLine("8. Revogar permissão de acesso ao usuario");
            Console.WriteLine("9. Registrar acesso");
            Console.WriteLine("10. Consultar logs de acesso");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 0) {
                cadastro.Upload();
                break;
            }

            switch (opcao) {
                case 1:
                    Console.Write("Digite o id do ambiente: ");
                    int idAmbiente = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do ambiente: ");
                    string nomeAmbiente = Console.ReadLine();
                    cadastro.AdicionarAmbiente(new Ambiente { Id = idAmbiente, Nome = nomeAmbiente });
                    Console.WriteLine("Ambiente cadastrado com sucesso.");
                    break;

                case 2:
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    var ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (ambiente != null) {
                        Console.WriteLine($"Ambiente encontrado: {ambiente.Nome}");
                    } else {
                        Console.WriteLine("Ambiente não encontrado.");
                    }
                    break;

                case 3:
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (ambiente != null && cadastro.RemoverAmbiente(ambiente)) {
                        Console.WriteLine("Ambiente removido com sucesso.");
                    } else {
                        Console.WriteLine("Falha ao remover ambiente.");
                    }
                    break;

                case 4:
                    Console.Write("Digite o id do usuário: ");
                    int idUsuario = int.Parse(Console.ReadLine());
                    Console.Write("Digite o nome do usuário: ");
                    string nomeUsuario = Console.ReadLine();
                    cadastro.AdicionarUsuario(new Usuario { Id = idUsuario, Nome = nomeUsuario });
                    Console.WriteLine("Usuário cadastrado com sucesso.");
                    break;

                case 5:
                    Console.Write("Digite o id do usuário: ");
                    idUsuario = int.Parse(Console.ReadLine());
                    var usuario = cadastro.PesquisarUsuario(idUsuario);
                    if (usuario != null) {
                        Console.WriteLine($"Usuário encontrado: {usuario.Nome}");
                    } else {
                        Console.WriteLine("Usuário não encontrado.");
                    }
                    break;

                case 6:
                    Console.Write("Digite o id do usuário: ");
                    idUsuario = int.Parse(Console.ReadLine());
                    usuario = cadastro.PesquisarUsuario(idUsuario);
                    if (usuario != null && cadastro.RemoverUsuario(usuario)) {
                        Console.WriteLine("Usuário removido com sucesso.");
                    } else {
                        Console.WriteLine("Falha ao remover usuário.");
                    }
                    break;

                case 7:
                    Console.Write("Digite o id do usuário: ");
                    idUsuario = int.Parse(Console.ReadLine());
                    usuario = cadastro.PesquisarUsuario(idUsuario);
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (usuario != null && ambiente != null && usuario.ConcederPermissao(ambiente)) {
                        Console.WriteLine("Permissão concedida com sucesso.");
                    } else {
                        Console.WriteLine("Falha ao conceder permissão.");
                    }
                    break;

                case 8:
                    Console.Write("Digite o id do usuário: ");
                    idUsuario = int.Parse(Console.ReadLine());
                    usuario = cadastro.PesquisarUsuario(idUsuario);
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (usuario != null && ambiente != null && usuario.RevogarPermissao(ambiente)) {
                        Console.WriteLine("Permissão revogada com sucesso.");
                    } else {
                        Console.WriteLine("Falha ao revogar permissão.");
                    }
                    break;

                case 9:
                    Console.Write("Digite o id do usuário: ");
                    idUsuario = int.Parse(Console.ReadLine());
                    usuario = cadastro.PesquisarUsuario(idUsuario);
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (usuario != null && ambiente != null) {
                        bool autorizado = usuario.Ambientes.Contains(ambiente);
                        ambiente.RegistrarLog(new Log(DateTime.Now, usuario, autorizado));
                        Console.WriteLine(autorizado ? "Acesso autorizado." : "Acesso negado.");
                    } else {
                        Console.WriteLine("Usuário ou ambiente não encontrado.");
                    }
                    break;

                case 10:
                    Console.Write("Digite o id do ambiente: ");
                    idAmbiente = int.Parse(Console.ReadLine());
                    ambiente = cadastro.PesquisarAmbiente(idAmbiente);
                    if (ambiente != null) {
                        Console.WriteLine("Filtrar por: 1. Autorizados 2. Negados 3. Todos");
                        int filtro = int.Parse(Console.ReadLine());
                        foreach (var log in ambiente.Logs) {
                            if (filtro == 1 && !log.TipoAcesso) continue;
                            if (filtro == 2 && log.TipoAcesso) continue;
                            Console.WriteLine($"{log.DtAcesso} - {log.Usuario.Nome} - {(log.TipoAcesso ? "Autorizado" : "Negado")}");
                        }
                    } else {
                        Console.WriteLine("Ambiente não encontrado.");
                    }
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
