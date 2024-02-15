using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Concessionaria_Auto
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                // Creo il comando per la stored procedure
                // RICEVE la stored procedure e la connessione
                // RESTITUISCE un oggetto SqlCommand che eseguirà la stored procedure
                SqlCommand cmd = new SqlCommand("getAuto", conn);

                // Indico che il comando è una stored procedure e non una query 
                cmd.CommandType = CommandType.StoredProcedure;

                // Eseguo il comando e salvo il risultato in un SqlDataReader 
                SqlDataReader reader = cmd.ExecuteReader();

                // Finchè ci sono auto nel db leggo e le aggiungo al select
                while (reader.Read())
                {
                    int id_auto = reader.GetInt32(0);
                    string marca = reader.GetString(1);
                    string modello = reader.GetString(2);

                    // Aggiungo le auto al select (innerHTML non è supportato dal Select) 
                    CarChoice.Items.Add(new ListItem($"{marca} {modello}", id_auto.ToString()));
                    
                }
            }
            catch (Exception ex)
            {

                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void CarChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCar = CarChoice.Text.ToString();
            CardCarSelected.InnerHtml = selectedCar;

            string connectionString = ConfigurationManager.ConnectionStrings["connectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                // Creo il comando per la stored procedure
                // RICEVE la stored procedure e la connessione
                // RESTITUISCE un oggetto SqlCommand che eseguirà la stored procedure
                SqlCommand cmd = new SqlCommand("getAutoByID", conn);

                // passo il parametro alla stored procedure (macchina selezionata)
                cmd.Parameters.AddWithValue("@id", selectedCar);

                // Indico che il comando è una stored procedure e non una query 
                cmd.CommandType = CommandType.StoredProcedure;

                // Eseguo il comando e salvo il risultato in un SqlDataReader 
                SqlDataReader reader = cmd.ExecuteReader();

                // Leggo i dati dell'auto selezionata (senza while si rompe)
                while (reader.Read())
                {
                    int id_auto = reader.GetInt32(0);
                    string marca = reader.GetString(1);
                    string modello = reader.GetString(2);
                    decimal prezzo = reader.GetDecimal(3);
                    string immagine = reader.GetString(4);

                    // Costruisci il markup HTML per la carta dell'auto selezionata
                    string cardHtml = $@"
                                        <div class='card' style='width: 18rem;'>
                                            <img src='{immagine}' class='card-img-top' alt=''>
                                            <div class='card-body'>
                                                <h5 class='card-title'>{marca} {modello}</h5>
                                                <p class='card-text'>Prezzo: {prezzo}</p>
                                                <a href='#' class='btn btn-primary'>Dettagli</a>
                                            </div>
                                        </div>";

                    // Imposta l'HTML generato nella sezione CardCarSelected
                    CardCarSelected.InnerHtml = cardHtml;

                }
            }
            catch (Exception ex)
            {

                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}