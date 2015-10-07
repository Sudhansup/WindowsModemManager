# WindowsModemManager
Window Service  using C#, Asp.Net to restart your modem when there is no internet

This is a window service written in C# to do the following tasks.

1) Checks if the System is connected to Any Netwrok or Not
2) If Connected, is it having Internet Access or Not by requesting to www.google.com and www.facebook.com
3) If Connected, and Not having internet access then Posts a request to restrt the Modem.


