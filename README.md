# 📡 Unity Photon Multiplayer Project

This project introduces **Photon Unity Networking (PUN)**, a Unity package for creating multiplayer games. It serves as a foundational step to learn multiplayer and networking concepts using Photon Cloud.

---

## 🧠 Learning Objectives

- Explain what Unity PUN is and its benefits.
- Understand the server/client relationship in Photon.
- Create the server-side setup using Photon.
- Implement the client-side setup using Photon.
- Synchronize data between server and clients in Photon.

---

## 🛠 Tasks Overview

### **0. Initialize New Project**
- Create a new Unity project.
- Import the Photon Unity Networking package from the Unity Asset Store.

### **1. Setup the Connection to Photon Cloud**
- Configure the `PhotonServerSettings` in the **Inspector**.
- Set the **App ID** to start using Photon features.

### **2. Create the Player Prefab**
- Import a rigged 3D character.
- Add a movement script to the player.
- Add UI components (name and health bar) above the player’s head.
- Implement shooting functionality (e.g., spheres as bullets).
- Ensure all elements are synchronized between server and clients.

### **3. Connect to Server (Join a Game)**
- Create a new scene and initialize a connection to the Photon Cloud server.
- Add a **Join Game** button with an InputField for the player’s name.
- Instantiate the player prefab when a client joins a game.

### **4. Play the Game**
- Allow two or more players to:
  - Move, rotate, and shoot bullets.
  - Synchronize health bars across the server and all clients.
- Implement damage logic so health decreases when a player is hit.

### **5. Kick Out**
- Players with `Health == 0` are kicked out of the game and redirected to the main menu.
- Display a "You Win!" message for the last player standing.

---

### **Setup Instructions**
1. Clone the repository:
   ```bash
   git clone https://github.com/holbertonschool-0x0P-unity-photon.git```
2. Open the project in Unity.
3. Import the Photon Unity Networking package from the Unity Asset Store.
4. Obtain your App ID from the Photon Dashboard and set it in PhotonServerSettings.

---

### How to Play

1. Launch the game and click Join Game.
2. Enter your player name and join a room.
3. Start moving, shooting, and competing with other players!