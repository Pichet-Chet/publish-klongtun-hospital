using System;
namespace KTH.MODELS.DocumentBuilder
{
	public class RefferalFeePosition
	{
		public RefferalFeePosition()
		{
            Items = new List<Items>();
        }

        public string? Position1 { get; set; }
        public string? Position2 { get; set; }
        public string? Position3 { get; set; }


        public List<Items> Items { get; set; }
    }

    public class Items
    {
        public string? ColA { get; set; }
        public string? ColB { get; set; }
        public string? ColC { get; set; }
        public string? ColD { get; set; }
    }
}

