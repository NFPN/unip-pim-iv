namespace BlackRiver.Data.Models
{
    public class Quarto
    {
        public int NumeroAndar { get; set; }
        public int NumeroQuarto { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Quarto quarto &&
                   NumeroAndar == quarto.NumeroAndar &&
                   NumeroQuarto == quarto.NumeroQuarto;
        }

        public override string ToString()
            => $"{NumeroAndar}{NumeroQuarto}";

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}