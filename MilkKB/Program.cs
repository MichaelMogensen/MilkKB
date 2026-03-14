using MilkKB.radio;
using MilkKB.radio.types;
using MilkKB.types;

var P4_1989_01_01 = 
    new ApiParams(
        new LocalFilename(@"c:\temp", "mp3", new BroadcastMetadata("P4 part one", new DateTime(1989, 1, 1, 19, 0, 0), TimeSpan.FromMinutes(45), "P4")),
        "a.mp3",
        "0_hjlxgbor");

var P4_1989_02_05 = 
    new ApiParams(
        new LocalFilename(@"c:\temp", "mp3", new BroadcastMetadata("P4", new DateTime(1989, 2, 5, 19, 0, 0), TimeSpan.FromHours(4), "P4")),
        "a.mp3",
        "0_lh4gezej");

var Shakespeare_og_den_hvide_jomfru_1989_02_15 = 
    new ApiParams(
        new LocalFilename(@"c:\temp", "mp3", new BroadcastMetadata("Shakespeare og den hvide jomfru", new DateTime(1986, 2, 15, 20, 30, 0), TimeSpan.FromMinutes(45), "P1")),
        "a.mp3",
        "0_w00a0b13");


/*
 Model:

var broadcastP4_1989_00_00 = new ApiParams(
    new LocalFilename(@"c:\temp", "mp3", new BroadcastMetadata("P4", new DateTime(1900, 1, 1, 19, 0, 0), TimeSpan.FromHours(4), "P4")),
    "a.mp3",
    "xxxxxxxxxx");


 */



//Console.WriteLine($"Call: {broadcastP4_1.Url}");
//Console.WriteLine($"Save as: {broadcastP4_1.FileOnLocal}");

await CallHost.DownloadStream(Shakespeare_og_den_hvide_jomfru_1989_02_15);


/*
 
<video id="_4urrq" class="playkit-engine playkit-engine-html5" tabindex="-1" playsinline="" disablepictureinpicture="" src="https://api.kltr.nordu.net/p/397/sp/39700/playManifest/entryId/0_lh4gezej/protocol/https/format/url/flavorIds/0_9cly4gn4/a.mp3?playSessionId=fcd3fb6b-61e8-8d83-34d9-e8dc0d422c94:8b7b905e-50e5-335f-99a6-117b4b2d4337&amp;referrer=aHR0cHM6Ly93d3cua2IuZGsvZmluZC1tYXRlcmlhbGUvZHItYXJraXZldC9wb3N0L2RzLnJhZGlvOm9haTppbzoxNWU0NTc4Yi02ZjBlLTRlMjMtODcyZS00MzY1MGM4YWRmMTc=&amp;clientTag=html5:v7.194"></video>
<video id="_esh06" class="playkit-engine playkit-engine-html5" tabindex="-1" playsinline="" disablepictureinpicture="" src="https://api.kltr.nordu.net/p/397/sp/39700/playManifest/entryId/0_w00a0b13/protocol/https/format/url/flavorIds/0_a849xn29/a.mp3?playSessionId=5c0cd8eb-4517-b929-2a6d-0d47b03859ca:0f4808af-90e4-b246-968c-4bbe240c3eef&amp;referrer=aHR0cHM6Ly93d3cua2IuZGsvZmluZC1tYXRlcmlhbGUvZHItYXJraXZldC9wb3N0L2RzLnJhZGlvOm9haTppbzpjNjJiMjk0Ni1kNTI1LTQ5Y2QtODI2Ny00NmIxNzQwZjQyOTY=&amp;clientTag=html5:v7.194"></video>

 */