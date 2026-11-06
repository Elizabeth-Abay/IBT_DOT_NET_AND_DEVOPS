// Telebirr top up calculator
// customer - name , mobile network , top-up amount
// parse the amt with TryParse - input - Nan , -ve , 0 , no > 5000
// switch to set the bonus - tele - 0.05 , safari - 0.03
// > 1000 , after the bonus
// print a receipt name , network  , amount , bonus , final  - N2 and aligned in column

const decimal TeleBirrBonusRate = 0.05m;
const decimal SafariBonusRate = 0.03m;
const int BonusForMoreThanThousand = 50;

string? userName = null;
string? networkProvider = null;
string? topUpAmount = null;
decimal topUp = 0m;
bool topUpConversionSuccess = false;


// check if the userName is null or not
while (userName == null)
{
    Console.WriteLine("Enter your name properly: ");
    userName = Console.ReadLine();
   
    userName = userName == "" ? null : userName; 
};

while (networkProvider == null)
{
    Console.WriteLine("Enter your network provider ( Safari or Tele): ");
    networkProvider = Console.ReadLine();

    networkProvider = networkProvider == "" ? null : networkProvider;
};

while (topUpAmount == null || !topUpConversionSuccess)
{
    Console.WriteLine("Enter top up amount in number greater than 0 and less than 5000: ");
    topUpAmount = Console.ReadLine();

    topUpAmount = topUpAmount == "" ? null : topUpAmount;

    // then parse the topUpAmount to decimal
    topUpConversionSuccess = decimal.TryParse(topUpAmount,out topUp);
    // it gives out boolean - true if the amount converted to decimal , false otherwise

    topUpConversionSuccess = topUpConversionSuccess && (topUp > 0) && (topUp < 5000);
};


// once converted the values
decimal bonus = networkProvider.ToLower() switch
{
    "safari" => topUp * SafariBonusRate,
    "tele" => topUp * TeleBirrBonusRate,
    _ => 0,
};


if (topUp >= 1000) {
    topUp += BonusForMoreThanThousand;
};

topUp += bonus;


Console.WriteLine($"Name : {userName}");
Console.WriteLine($"Network Provider : {networkProvider}");
Console.WriteLine($"TopUp Amount Total : {topUp}");
Console.WriteLine($"Bonus from provider : {bonus}");
Console.WriteLine("Thank you for choosing us");






