using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Data.SqlClient;

namespace PracaDyplomowa
{

    public static class DataBaseManager
    {
        private static string selectQuery;

        public static void DBMselect (string connectionString, string attributes, string table, string condition = null)
        {
            try
            {
                // Utworzenie obiektu połączenia
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otwarcie połączenia
                    connection.Open();

                    // Zdefiniowanie polecenia SQL SELECT
                    if (condition == null)
                    {
                        selectQuery = $"SELECT {attributes} FROM {table}";
                    }
                    else { selectQuery = $"SELECT {attributes} FROM {table} WHERE {condition}"; }
                    // Utworzenie obiektu polecenia SQL
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Utworzenie obiektu czytnika danych
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Sprawdzenie, czy są dostępne wiersze
                            if (reader.HasRows)
                            {
                                // Odczytanie danych
                                while (reader.Read())
                                {
                                    // Pobranie danych z kolumn
                                    string value1 = reader.GetString(0);
                                    int value2 = reader.GetInt32(1);

                                    // Wyświetlenie pobranych danych (tutaj możesz dostosować sposób wykorzystania danych)
                                   // Console.WriteLine($"Wartość {columnName1}: {value1}, Wartość {columnName2}: {value2}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Brak danych do wyświetlenia.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
        public static void DBMinsert(string connectionString, string table, List<string> values)
        {
            try
            {
                // Utworzenie obiektu połączenia
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otwarcie połączenia
                    connection.Open();

                    // Zdefiniowanie polecenia SQL INSERT z parametrami
                    string insertQuery = $"INSERT INTO {table} VALUES (@Value1)";

                    // Utworzenie obiektu polecenia SQL
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Dodanie parametrów do polecenia
                        command.Parameters.AddWithValue("@Value1", values);

                        // Wykonanie polecenia INSERT
                        int rowsAffected = command.ExecuteNonQuery();

                        // Sprawdzenie, czy wstawienie danych się powiodło
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Polecenie INSERT zostało wykonane pomyślnie.");
                        }
                        else
                        {
                            Console.WriteLine("Wystąpił problem podczas wykonywania polecenia INSERT.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
        public static void DBMdelete(string connectionString, string table, string condition)
        {
            try
            {
                // Utworzenie obiektu połączenia
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otwarcie połączenia
                    connection.Open();

                    // Zdefiniowanie polecenia SQL DELETE z warunkiem
                    string deleteQuery = $"DELETE FROM {table} WHERE {condition}";

                    // Utworzenie obiektu polecenia SQL
                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {

                        // Wykonanie polecenia DELETE
                        int rowsAffected = command.ExecuteNonQuery();

                        // Sprawdzenie, czy usunięcie danych się powiodło
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Polecenie DELETE zostało wykonane pomyślnie.");
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono pasujących danych do usunięcia.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }

        public static void DBMupdate(string connectionString, string column, string table, string value,  string condition)
        {
            try
            {
                // Utworzenie obiektu połączenia
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Otwarcie połączenia
                    connection.Open();

                    // Zdefiniowanie polecenia SQL UPDATE z warunkiem
                    string updateQuery = $"UPDATE {table} SET {column} = {value} WHERE {condition}";

                    // Utworzenie obiektu polecenia SQL
                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {

                        // Wykonanie polecenia UPDATE
                        int rowsAffected = command.ExecuteNonQuery();

                        // Sprawdzenie, czy aktualizacja danych się powiodła
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Polecenie UPDATE zostało wykonane pomyślnie.");
                        }
                        else
                        {
                            Console.WriteLine("Nie znaleziono pasujących danych do aktualizacji.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
            }
        }
    }
}
