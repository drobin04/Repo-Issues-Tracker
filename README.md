# Repo Issues Tracker
 The purpose of this app is to allow you to quickly view or add to the open issues for your various github repo's. 
 
 ![Screenshot](https://i.imgur.com/0va9051.png)
 ![Screenshot 2](https://i.imgur.com/whQsepg.png)
 
 # Features
 * Can select any public/private repo on your account, or a manually specified 3rd party repo you have access to, and will populate a list of open issues on the repo.
 * Can double-click any open issue to pop open a dialog with details and aggregated comments for the issue
 * Can Add Comments To An Existing Issue
 * Button to launch browser to currently opened Github Issue directly
 * Can Create New Issues for selected Repo
 * Can Close a selected issue as completed from within the app, allowing for easily viewing/creating/closing issues without having to interact with github in a browser.
 
 # Setup
 
 You can download the setup / installer files [Here](Releases/)
 
 Upon the first run of the application, you will be prompted for a GitHub Personal Access Token (PAT). 

 You can create a PAT by opening GitHub, and navigating to Settings > Developer Settings > Personal Access Tokens.

 You can either use the older style personal access tokens, or you can use fine-grained personal access tokens that have read/write permissions for repo's and issues. 

# Linux Support

This app has been tested to work on Linux! Haha, sort of. I wrote it in .NET Framework because I didn't have very much time to do so in another language that would actually be cross-platform.

You will need to install *wine*, *winetricks*, and then .NET Desktop Runtime 6.0 using winetricks. 

After this, the application should work flawlessly.

^ Tested on Linux Mint 21.3 & Linux Mint 22.