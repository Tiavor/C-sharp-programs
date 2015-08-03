# C-sharp-programs
STO-Partikel
 - a bot for farming a mini game in StarTrek Online. the setup is not finished.
   the bot it self works fine for me. I added the setup later that it is
   adjustable for other users that might have different resolutions, color
   scheme and window size in the game. Because it is that variable, it was
   hard to program and test for all variations.

GW2Helper
 - manage multiple gw2 accounts, stores login information encrypted.
   but the transmission of the password to the client is unsafe.
 - currently working on a better version that useses the built-in
   login information storage in %appdata%, but it will have a more
   complicated setup
   
PingClient
 - I had disconnects from the internet on a regular basis so I wrote this
   to check the connection between my pc and another device in my network.
   In this case the other device iy my Raspberry Pi2.
   I wanted to know if the problem is in my network somewhere or external
 - somehow the program didn't show any disconnects in the first two months
   then I had 2 weeks almost no DCs but when they came back I tested it again
   and finally my program showed also disconnects. even though I didn't manage
   to recover the ping from the DCs it was evidence enough for me that the
   problem was in my network and not at the provider.
 - I put a WLAN card in my pc and a WLAN router half a meter beside it, now
   it works like a charm, not a single DC since that day.
   I had this card because in my last appartment I had only acces to WLAN.
   Seems like my onboard network controller is broken. It is an old mainboard
   and I had a broken power supply only a few weeks before the DCs started.
