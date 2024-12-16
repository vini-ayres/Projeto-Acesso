using System;

public class Log {
    public DateTime DtAcesso { get; set; }
    public Usuario Usuario { get; set; }
    public bool TipoAcesso { get; set; } // true=Autorizado, false=Negado

    public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso) {
        DtAcesso = dtAcesso;
        Usuario = usuario;
        TipoAcesso = tipoAcesso;
    }
}
