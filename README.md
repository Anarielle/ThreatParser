# Threat Parser
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/6b7b630c12074adf81f9df4bce536b60)](https://www.codacy.com/gh/Anarielle/ThreatParser/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Anarielle/ThreatParser&amp;utm_campaign=Badge_Grade)

The application automatically creates a local database of information threats by downloading and then parsing information from the official threat database of the FSTEC of Russia.

## Features
### Viewing the general list of threats

Displays short information about threats in the form of a threat ID and name.

<div align="center">

![Alt text](./Resources/Parser_short_info.png) 

</div>

### View all details about each threat

Each information security threat record includes the following information:

-   identifier
-   name
-   description
-   source
-   object of influence
-   privacy violation (yes/no)
-   violation of integrity (yes / no)
-   accessibility violation (yes/no)

<div align="center">

![Alt text](./Resources/Parser_detailed_info.png) 

</div>

### Update information

Automatic updating of information (at the request of the user), as a result of which the program displays a report on updated records.

<div align="center">

![Alt text](./Resources/Parser_update_info.png) 

</div>

## Technologies
-   .NET Fraemwork 4.7.2
-   WPF
-   C#
-   EPPlus library version 6.0

