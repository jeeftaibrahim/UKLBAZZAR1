//Parent Class
using System.Buffers;
using System.Collections.Concurrent;

class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _IsAvailable;

    public Stand(string namaStand, double hargaSewaPerHari)
    {
        NamaStand = namaStand;
        HargaSewaPerHari = hargaSewaPerHari;
        _IsAvailable = true;
    }

    //property nama stand
    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("nama stand tidak boleh kosong atau di isi spasi.");
            _namaStand = value;
        }

    }

    //property sewa
    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari;}
        set
        {
            if (value <= 0)
                throw new ArgumentException("harga sewa per hari harus lebih dari 0");
            _hargaSewaPerHari = value;
        }
    }

    //property isavailable get only
    public bool IsAvailable
    {
        get { return _IsAvailable; }
    }

    //method display info
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Nama stand: {_namaStand}");
        Console.WriteLine($"Harga/hari: Rp. {_hargaSewaPerHari}");
        Console.WriteLine($"Status: {(_IsAvailable ? "Tersedia" : "Disewa")}");
    }

    //method ubahsattus
    public void UbahStatus()
    {
        _IsAvailable = !_IsAvailable;
    }

    //method virtual hitung total
    public virtual double HitungTotal(int jumlahHari)
    {
        return _hargaSewaPerHari * jumlahHari;
    }

    
}
//class stand outdoor
class StandOutdoor : Stand
{
    protected double _biayaTenda = 75000;

    public StandOutdoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaTenda
    {
        get { return _biayaTenda; }
    }

    //override hitung total
    public override double HitungTotal(int jumlahHari)
    {
        return (_hargaSewaPerHari * jumlahHari) + (_biayaTenda * jumlahHari);
    }
}

//class indoor
class StandIndoor : Stand
{
    protected double _biayaListrik = 100000;

    public StandIndoor(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {

    }
    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }

    //override hitung total
    public override double HitungTotal(int jumlahHari)
    {
        return (_hargaSewaPerHari * jumlahHari) + (_biayaListrik * jumlahHari);
    }
}

class StandPremium : Stand
{
    protected double _biayaKeamanan = 300000;

    public StandPremium(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaKeamanan
    {
        get { return _biayaKeamanan; }
    }

    //override hitung total
    public override double HitungTotal(int jumlahHari)
    {
        return (_hargaSewaPerHari * jumlahHari) + _biayaKeamanan;
    }
}

class StandVvip : Stand
{
    protected double _biayaParkir = 300000;

    public StandVvip(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaParkir
    {
        get { return _biayaParkir; }
    }

    //override hitung total
    public override double HitungTotal(int jumlahHari)
    {
        return (_hargaSewaPerHari * jumlahHari) + _biayaParkir;
    }
}


class program
{
    static void Main(string[] args)
    {
        List<Stand> daftarStand = new List<Stand>
        {
            new StandOutdoor ("Outdoor-1", 400000),
            new StandOutdoor ("Outdoor-2", 500000),
            new StandIndoor ("Indoor-1", 700000),
            new StandIndoor ("Indoor-2", 800000),
            new StandPremium ("Premium-1", 1800000),
            new StandPremium ("Premium-2", 2000000),
            new StandVvip ("VVIP-1", 3000000)
        };

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Moklet Expo Management Center");
            Console.WriteLine("Daftar stand tersedia");
            Console.WriteLine();

            foreach (Stand s in daftarStand)
            {
                if (s.IsAvailable)
                {
                    Console.WriteLine($" {s.NamaStand,-12} | Rp {s.HargaSewaPerHari,10:N0} / hari | tersedia");
                }
            }
            Console.WriteLine();
            Console.WriteLine("1. sewa stand");
            Console.WriteLine("2. akhiri sewa stand");
            Console.WriteLine("3. keluar");
            Console.WriteLine();
            Console.Write("Pilih menu: ");

            string pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    SewaStand(daftarStand);
                    break;
                case "2":
                    AkhiriSewa(daftarStand);
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Moklet expo management center");
                    Console.WriteLine("daftar stand tersedia");
                    Console.WriteLine();
                    foreach (Stand s in daftarStand)
                    {
                        if (s.IsAvailable)
                            Console.WriteLine($" {s.NamaStand,-12} | Rp {s.HargaSewaPerHari,10:N0} /hari | tersedia");
                    }
                    Console.WriteLine();
                    Console.WriteLine("1. sewa stand");
                    Console.WriteLine("2. akhiri sewa stand");
                    Console.WriteLine("3. keluar");
                    Console.WriteLine();
                    Console.WriteLine($"Pilih menu: 3)");
                    Console.WriteLine();
                    Console.WriteLine("terima kasih");
                    Console.Write("tekan enter");
                    Console.ReadLine();
                    running = false;
                    break;
                default:
                    Console.WriteLine("pilihan tidak valid tekan enter");
                    Console.ReadLine();
                    break;

            }




        }
    }

    static void SewaStand(List<Stand> daftarstan)
    {
        Console.WriteLine("\nmasukan nama stand: ");
        string inputNama = Console.ReadLine();

     

        Stand stand = daftarstan.FirstOrDefault(
            s => s.NamaStand.Equals(inputNama, StringComparison.OrdinalIgnoreCase));

        if (stand == null)
        {
            Console.WriteLine("stand tidak ditemukan");
        }
        else if (!stand.IsAvailable)
        {
            Console.WriteLine("stand tidak tersedia");
        }
        else
        {
            Console.WriteLine($"stand ditemukan: {stand.NamaStand} | Rp {stand.HargaSewaPerHari:N0} / hari");
            Console.Write("masukan jumlah hari: ");

            if (int.TryParse(Console.ReadLine(), out int jumlahHari) && jumlahHari > 0)
            {
                double totalBiaya = stand.HitungTotal(jumlahHari);
                Console.WriteLine($"\ntotal biaya: Rp {totalBiaya:N0}");

                stand.UbahStatus();
                Console.WriteLine($"Stand {stand.NamaStand} berhasil disewakan selama {jumlahHari} hari");
            }
            else
            {
                Console.WriteLine("jumlah hari tidak valid");
            }

        }
        Console.Write("Tekan enter");
        Console.ReadLine();
    }

    static void AkhiriSewa(List<Stand> daftarstand)
    {
        Console.WriteLine("\n daftar stand yang disewakan");
        foreach (Stand s in daftarstand)
        {
            if (!s.IsAvailable)
            {
                Console.WriteLine($" {s.NamaStand,-12} | Rp {s.HargaSewaPerHari,10:N0} / hari | tidak tersedia");
            }
        }
        Console.Write("\nmasukan nama stand: ");
        string inputNama = Console.ReadLine();

        Stand stand = daftarstand.FirstOrDefault(
            s => s.NamaStand.Equals(inputNama, StringComparison.OrdinalIgnoreCase));

        if (stand == null)
        {
            Console.WriteLine("Stand tidak ditemukan");
        }
        else if (stand.IsAvailable)
        {
            Console.WriteLine("Stand belum disewa");
        }
        else
        {
            Console.WriteLine($"stand ditemukan {stand.NamaStand} | Rp {stand.HargaSewaPerHari:N0} / hari");
            stand.UbahStatus();
            Console.WriteLine($"sewa stand {stand.NamaStand} berhasik diakhiri");
        }

        Console.Write("tekan enter");
        Console.ReadLine();


    }
}