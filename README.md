BusTracker1.0 (Virginia Tech Bus Tracker for Windows Phone 8.0)
==========

![VT MAD](http://www.mad.org.vt.edu/sites/default/files/MAD_Logo_Smaller_0.png)

Guide on How to Clone and Import theVT MAD Windows Phone Bus Tracker Project into Visual Studio 2010
==========

This app will be used to track the buses around the campus using the BT4U [API](http://www.bt4u.org/BT4U_WebService.asmx).

## Before We Ge Started..

**Please Make Sure You Have The Following Set Up**.. 
All downloads can be found [here](http://sdrv.ms/NMNtMr).

- Windows 8 Installed
- Windows Phone 8.0 SDK Installed
 
You have the choice of using Git either with [Git Bash](https://github.com/jason-riddle/BusTracker#option-1-setting-up-git-bash) or [GitHub for Windows](https://github.com/jason-riddle/BusTracker#https://github.com/jason-riddle/BusTracker#option-2-github-for-windows). I will explain how to set up both, but you are only required to set up **one**.

## (Option 1) Setting up Git Bash

#### Open Git Bash and type in the following command

    cd C:\Users\%USERNAME%\"My Documents"\"Visual Studio 2010"\Projects
    
#### Next, we will clone the git repository

    git clone https://github.com/avneet723/BusTracker1.0.git
    
We need to give Git some basic information about ourselves

#### Let's navigate into our git repository we just cloned

    cd BusTracker1.0
    
 Set up our name and email.. For example, If my name is `John Doe` and my email `johndoe@vt.edu`

    git config user.name "John Doe"
    git config user.email "johndoe@vt.edu"    
    
**This Ends Setting Up Git with Git Bash**
    
## (Option 2) Setting up GitHub for Windows

#### Open GitHub and sign in with your credentials

Go to `Tools` -> `Options`

Two things we will want to change are `configure git` and `default storage directory`

For `configure git`, we need to set up our name and email..  For example, If my name is `John Doe` and my email `johndoe@vt.edu`

#### In the first block

    John Doe
    
#### In the next block

    johndoe@vt.edu

For `default storage directory`, we need to tell git where we want our projects stored.

Click the following symbol `...` and navigate to 

    My Documents\Visual Studio 2010\Projects

This is where we will be storing Visual Studio projects

**This Ends Setting Up Git with GitHub for Windows**

## Importing Our Project into Visual Studio

Open up Visual Studio 2010 Express

Click on `Open Project`

Go to Your My Documents Folder -> Visual Studio 2010 -> Projects -> BusTracker1.0 Folder

Click on (either) BusTracker File and click `Open`

Done!
    
## Actually Using Git

Make some changes to your code

#### Next, we want to add those changes into the "index"
    
    git add .
    
#### Now "commit" and add a message

    git commit -m "Fixed the Terrible Bug"
    
#### Now let's "push" it to our GitHub Repository

    git push 

Done!  