using Microsoft.Data.Sqlite;
using ScreenSound.Modelos;

namespace ScreenSound.Banco;

internal class ArtistaDAL
{

    private SqliteConnection _connection = new Connection().ObterConexao();

    public void Listar()
    {
        var lista = new List<Artista>();
        _connection.Open();

        string sql = "SELECT * FROM Artistas";
        SqliteCommand command = new SqliteCommand(sql, _connection);
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

        _connection.Close();
    }

    public void Adicionar(Artista artista)
    {
        _connection.Open();

        string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
        SqliteCommand command = new SqliteCommand(sql, _connection);

        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
        _connection.Close();
    }

    public void Atualizar(Artista artista)
    {
        _connection.Open();

        string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio WHERE Id = @id";
        SqliteCommand command = new(sql, _connection);

        command.Parameters.AddWithValue("@id", artista.Id);
        command.Parameters.AddWithValue("@nome", artista.Nome);
        command.Parameters.AddWithValue("@bio", artista.Bio);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
        
        _connection.Close();
    }

    public void Deletar(int id)
    {
        _connection.Open();

        string sql = "DELETE FROM Artistas WHERE Id = @id";
        SqliteCommand command = new(sql, _connection);

        command.Parameters.AddWithValue("@id", id);

        int retorno = command.ExecuteNonQuery();
        Console.WriteLine($"Linhas afetadas: {retorno}");
        _connection.Close();
    }

    public Artista? ObterPorId(int id)
    {
        _connection.Open();

        string sql = "SELECT * FROM Artistas WHERE Id = @id";
        SqliteCommand command = new(sql, _connection);

        command.Parameters.AddWithValue("@id", id);

        SqliteDataReader dataReader = command.ExecuteReader();

        if (dataReader.HasRows)
        {
            dataReader.Read();

            Artista dbArtista = new(
                id: Convert.ToInt32(dataReader["Id"]),
                bio: Convert.ToString(dataReader["Bio"])!,
                nome: Convert.ToString(dataReader["Nome"])!,
                fotoPerfil: Convert.ToString(dataReader["FotoPerfil"])!
            );

            return dbArtista;
        }

        return null;
    }
}
