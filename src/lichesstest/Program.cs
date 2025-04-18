string moves = "e4,e5,Nf3,Nc6,Bb5";
var openingService = new OpeningService();
string openingName = await openingService.GetOpeningNameAsync(moves);
Console.WriteLine($"Opening: {openingName}");