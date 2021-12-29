using Fondital.Data;
using Fondital.Server.Controllers;
using Fondital.Shared.Dto;
using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using Fondital.Shared.Models.Auth;
using System.Threading.Tasks;

namespace Fondital.Server
{
    public class SeedData
    {
        protected static MailController _mailController { get; set; }

        public SeedData(MailController mailController)
        {
            _mailController = mailController;
        }

        public static async Task Initialize(FonditalDbContext db)
        {
            var configurazioni = new Configurazione[]
            {
                new Configurazione()
                {
                    Chiave = "DurataGaranzia",
                    Valore = DurataValiditaConfigurazione.DueMesi.ToString(),
                },
                new Configurazione()
                {
                    Chiave = "DurataPassword",
                    Valore = DurataValiditaConfigurazione.DueMesi.ToString(),
                }
            };

            var ruoli = new Ruolo[]
            {
                new Ruolo()
                {
                    Name = "Direzione"
                },
                new Ruolo()
                {
                    Name = "Service Partner"
                }
            };

            var utente = new UtenteDto()
            {
                //dati della prima utenza
                UserName = "",
                Email = "",
                Nome = "",
                Cognome = ""
            };

            db.Configurazioni.AddRange(configurazioni);
            db.Ruoli.AddRange(ruoli);
            db.SaveChanges();

            //await _mailController.SendMailForNewUser(utente);
        }
    }
}