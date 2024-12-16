using System;
using System.Collections.Generic;

public class Usuario {
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Ambiente> Ambientes { get; set; } = new List<Ambiente>();

    public bool ConcederPermissao(Ambiente ambiente) {
        if (!Ambientes.Contains(ambiente)) {
            Ambientes.Add(ambiente);
            return true;
        }
        return false;
    }

    public bool RevogarPermissao(Ambiente ambiente) {
        return Ambientes.Remove(ambiente);
    }
}
