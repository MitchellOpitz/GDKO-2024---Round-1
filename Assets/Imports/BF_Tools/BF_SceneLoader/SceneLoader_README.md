## Table of Contents

- [Introduction](#introduction)
- [SceneLoader Script](#sceneloader-script)
- [Fader Script](#fader-script)

## Introduction

This repository contains Unity scripts for scene loading and fading effects, designed to enhance the user experience in your Unity projects. The scripts include:

- **SceneLoader**: A script to facilitate seamless scene transitions with fade-in and fade-out effects.

- **Fader**: A script responsible for handling fade-in and fade-out animations.

These scripts are designed to be user-friendly and can be easily integrated into your Unity projects to create smoother and more visually appealing transitions between scenes.

## SceneLoader Script

The `SceneLoader` script provides a simple way to load scenes with fade-in and fade-out effects. It includes a singleton pattern for easy access and integration.

### Features

- Singleton pattern for easy access.
- Seamless scene transitions with fading effects.
- Handles missing references and provides options for immediate scene loading.

## Fader Script

The `Fader` script is responsible for managing fade-in and fade-out animations. It is designed to be used in conjunction with the `SceneLoader` script but can also be used independently for various visual effects.

### Features

- Control over fade-in and fade-out animations.
- Customizable fade duration and fade color.
- Easily integrates with other Unity scripts and components.

## Usage

To use the `SceneLoader` and `Fader` scripts in your Unity project:

1. Attach the `SceneLoader` and `Fader` scripts to GameObjects in your scene.
2. Set up the necessary references, such as the fade image, in the Inspector.
3. Call the `LoadSceneByName` method from your scripts to initiate scene transitions with fade effects.