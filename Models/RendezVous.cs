namespace PFA_RDV_Medecin.Models
{
    public class RendezVous
    {
        public int Id { get; set; }

        public int MedecinId { get; set; }
        public virtual Medecin Medecin { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public DateTime Date { get; set; }
    }
}
