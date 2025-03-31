using Microsoft.Data.Sqlite;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{
    public void Listar()
    {
        var lista = new List<Artista>();
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "SELECT * FROM Artistas";
        SqliteCommand command = new SqliteCommand(sql, connection);
        using SqliteDataReader dataReader = command.ExecuteReader();

        while (dataReader.Read())
        {
            string nomeArtista = Convert.ToString(dataReader["Nome"])!;
            string bioArtista = Convert.ToString(dataReader["Bio"])!;
            int idArtista = Convert.ToInt32(dataReader["Id"])!;
            Artista artista = new(nomeArtista, bioArtista) { Id = idArtista };

            lista.Add(artista);
        }

        foreach (var artista in lista)
        {
            Console.WriteLine(artista);
        }
    }

    public void Adicionar(Artista artista)
    {
        using var connection = new Connection().ObterConexao();
        connection.Open();

        string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
        SqliteCommand command = new SqliteCommand(sql, connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
    }
}
