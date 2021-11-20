# Dark Souls III Save Backup Tool

A tool that automatically copies your current DS3 save to a folder called "DS3 Save Backup" in your documents directory.
To use it, just open the shortcut called "DS3 Save Backup Tool" in the folder.

# Additional instructions for making the program run when you start the game
*Note: this doesn't really work since it seems to send an Audit process event while the game is running as well, causing your save to back up repeatedly as you play, which kind of breaks the point of this since it's meant to give you saves from the start of your session in case you get hacked.*

*But I'm just going to leave it here anyway ¯\\_(ツ)_/¯*

1. Press Win + R and enter `secpol.msc`

2. Navigate to Local Policies/Audit Policy

3. Click "Audit process tracking" and tick the checkbox behind "Success"

4. Open Task Scheduler

5. Click "Create Task"

6. Name the task in the *General* tab

7. Go to the *Triggers* tab and create a new trigger

8. Choose "On an event" as the trigger, then click Custom

9. Click XML and then check "Edit query manually"

10. Copy and paste the following into the editor:

```xml
<QueryList>
	<Query Id="0" Path="Security">
		<Select Path="Security">*[System[Provider[@Name='Microsoft-Windows-Security-Auditing'] and Task = 13312 and (band(Keywords,9007199254740992)) and (EventID=4688)]] and *[EventData[Data[@Name='NewProcessName'] and (Data= <!--- path to game here ---> 'C:\Program Files (x86)\Steam\steamapps\common\DARK SOULS III\Game\DarkSoulsIII.exe')]]</Select>
	</Query>
</QueryList>
```

11. Click *Ok* to close the dialog boxes, then go to the *Actions* tab

12. Create a new Action, and select it as "Start a Program"

13. Browse to the directory with the downloaded folder, then select the .exe file in DS3SaveBackup/bin/Debug/netcoreapp3.1/

The program should run every time DS3 starts now.
