# Repo Issues Tracker
 The purpose of this app is to allow you to quickly view or add to the open issues for your various github repo's. 
 
 ![Screenshot](https://i.imgur.com/0cDeLPr.png)
 
 # Features
 * Can select any public/private repo on your account and will populate a list of open issues on the repo.
 * Can double-click any open issue to pop open a dialog with details and aggregated comments for the issue
 * (Coming Soon) Button to launch browser to this Github Issue directly
 * Can Create New Issues for selected Repo
 * Can Close a selected issue as completed from within the app, allowing for easily viewing/creating/closing issues without having to interact with github in a browser.
 
 # Setup
 
 You can download the setup / installer files [Here](Releases/)
 
 Upon the first run of the application, you will be prompted for a Client ID and Client Secret for a GitHub OAuth Application. 
 
 You will need to set up an App for this under the Developer Settings page of your github account in order to retrieve these. 
 
 Here's a link: https://github.com/settings/developers
 
 You'll want to create a new OAuth App, and set the Callback URL to http://localhost:5000/ 
 
 You can set the homepage URL that you're required to enter to this project's Github if you wish.

# Linux Support

This app has been tested to work on Linux! Haha, sort of.

You will need to install *wine*, *winetricks*, and then .NET Desktop Runtime 6.0 using winetricks. 

After this, the application should work flawlessly.

^ Tested on Linux Mint 21.3 & Linux Mint 22.