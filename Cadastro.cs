using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class Cadastro {
    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
    public List<Ambiente> Ambientes { get; set; } = new List<Ambiente>();

    public void AdicionarUsuario(Usuario usuario) {
        Usuarios.Add(usuario);
    }

    public bool RemoverUsuario(Usuario usuario) {
        if (usuario.Ambientes.Count == 0) {
            return Usuarios.Remove(usuario);
        }
        return false;
    }

    public Usuario PesquisarUsuario(int id) {
        return Usuarios.Find(u => u.Id == id);
    }

    public void AdicionarAmbiente(Ambiente ambiente) {
        Ambientes.Add(ambiente);
    }

    public bool RemoverAmbiente(Ambiente ambiente) {
        return Ambientes.Remove(ambiente);
    }

    public Ambiente PesquisarAmbiente(int id) {
        return Ambientes.Find(a => a.Id == id);
    }

    public void Upload() {
        XmlSerializer serializer = new XmlSerializer(typeof(Cadastro));
        using (TextWriter writer = new StreamWriter("cadastro.xml")) {
            serializer.Serialize(writer, this);
        }
    }

    public void Download() {
        if (File.Exists("cadastro.xml")) {
            XmlSerializer serializer = new XmlSerializer(typeof(Cadastro));
            using (TextReader reader = new StreamReader("cadastro.xml")) {
                Cadastro cadastro = (Cadastro)serializer.Deserialize(reader);
                this.Usuarios = cadastro.Usuarios;
                this.Ambientes = cadastro.Ambientes;
            }
        }
    }
}
