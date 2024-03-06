# Effective Communication UWP

This UWP application is a GUI interface to interact with this [project](https://github.com/Captainfl4me/mbed-effective-communication).

The goal of this project is to understand how quickly build a Windows UWP app to interface an embedded system using Serial Bus communication.

## Features

This application is very basic and provides two pages for the user. The first one is a sync page where you can request for current status of the microcontroller (mode and current led power). The update is manuals using the "sync" button. The second page allow you to send differents modes and values to the connected device and read incoming error messages if there are ones.

This application is also built with security to prevent unexpected behavior if the STM board is not plugged in or become unavailable during runtime.
