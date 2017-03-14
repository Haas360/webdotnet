# webdotnet
Project for "Daj się poznać" initiative

Blog website: http://www.webdotnet.pl

# how to run this locally
1. Download sources from Git
2. Create Db default name is "Webdotnet_umbraco" if you want anything else, remember to change the connection string in Web.Config Webdotnet.Umbraco project
3. Restore Db from file in Webdotnet.Db folder (I've used SQL 2012)
3. I host website on IIS but you can use IIS express (a default after pressing "run in [yourdefault browser]" button in VS)
4. Site should run and you should get homepage
5. If you want to explore Umbraco back office you need to:
	* enter [yourdomain]/umbraco
	* Login: "Admin"
	* Password: "qwerty123"
6. To compile frontend you need to:
	* Go to Webdotnet.Frontend folder
	* "shift + right" click inside and click open comand window here (or go to this catalog incmd/powershell)
	* install gulp globally (command: "npm install gulp -g --save")
	* type "gulp" in command line and press enter
6. ...More come soon :) But just to run and explore things this guide is enough :)
