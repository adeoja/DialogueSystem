# Unity Dialogue System

A designer-friendly dialogue system for Unity that uses ScriptableObjects for easy content creation and a typewriter effect for immersive text display.

<img src="https://github.com/user-attachments/assets/d4b98f50-a64e-4750-a9b9-76f55639d69b" width="400">


## Features

- **ScriptableObject-based architecture** - Create and manage dialogue content directly in the Unity Editor
- **Typewriter effect** - Character-by-character text reveal with configurable speed
- **Skip functionality** - Press Space to instantly reveal text or advance to the next line
- **Singleton pattern** - Easy global access to the dialogue manager
- **Click-to-interact** - Simple mouse-based interaction system
- **Modular design** - Plug-and-play components that work together seamlessly

## System Architecture

The system is built on four core scripts:

### DialogueContainer
A ScriptableObject that holds an array of dialogue lines, allowing you to create complete conversations as reusable assets.

### DialogueLine
Individual dialogue entries containing speaker name and dialogue text. These can be mixed and matched across different containers.

### DialogueManager
Singleton manager that handles:
- Displaying dialogue UI
- Typewriter text animation
- Input handling (Space to advance/skip)
- Dialogue flow control

### Interactable
Component that triggers dialogue when 2D objects are clicked, connecting game objects to dialogue content.

## Setup Instructions

### 1. Create Dialogue Content

#### Create Individual Dialogue Lines
1. Right-click in the Project window
2. Navigate to **Create → Scriptable Objects → Dialogue**
3. Name your dialogue line (e.g., "Hero_Line1")
4. In the Inspector, fill in:
   - **Speaker Name**: Character's name (e.g., "Hero")
   - **Dialogue Line**: The text they say (e.g., "I need to find the ancient artifact!")

#### Create a Dialogue Container
1. Right-click in the Project window
2. Navigate to **Create → Scriptable Objects → DialogueContainer**
3. Name your container (e.g., "QuestGiver_Conversation")
4. In the Inspector, set the **Dialogue Set** array size
5. Drag your DialogueLine assets into the array slots in order

**Example Container Structure:**
```
DialogueContainer: "QuestGiver_Conversation"
├── DialogueLine 0: "QuestGiver_Greeting"
├── DialogueLine 1: "QuestGiver_Request"
└── DialogueLine 2: "QuestGiver_Farewell"
```

### 2. Setup Dialogue UI

1. Create a **Canvas** in your scene (UI → Canvas)
2. Add a **Panel** as a child of Canvas for the dialogue box background
3. Inside the Panel, add two **TextMeshProUGUI** elements:
   - One for the speaker name (e.g., "SpeakerText")
   - One for the dialogue content (e.g., "DialogueText")
4. Position and style your UI elements as desired

### 3. Setup Dialogue Manager

1. Create an empty GameObject in your scene (name it "DialogueManager")
2. Add the **DialogueManager** script to it
3. In the Inspector, assign:
   - **Dialogue Panel**: Your Panel GameObject
   - **Speaker Text**: Your speaker TextMeshProUGUI
   - **Dialogue Text**: Your dialogue TextMeshProUGUI
   - **Type Speed**: Animation speed (default: 0.05, lower = faster)
4. Disable the Dialogue Panel by default (uncheck it in the hierarchy)

### 4. Make Objects Interactable

1. Select the 2D GameObject you want to make interactable (e.g., an NPC sprite, quest item, sign)
2. Ensure it has a **Collider2D** component (Box Collider 2D, Circle Collider 2D, Polygon Collider 2D, etc.)
3. Add the **Interactable** script to the GameObject
4. In the Inspector, assign your **DialogueContainer** to the script
5. Click the object in Play mode to trigger dialogue

**Note:** The object must have a Collider2D for mouse detection to work properly.

## How to Use

### Basic Player Interaction

1. Enter Play mode
2. **Click** on any object with an Interactable component
3. Dialogue will appear with typewriter effect
4. **Press Space** to:
   - Complete the current line instantly (if still typing)
   - Advance to the next line (if typing is complete)
5. Dialogue automatically closes after the last line

### Triggering Dialogue from Code

```csharp
// From any script, trigger dialogue programmatically
public DialogueContainer myDialogue;

void TriggerConversation()
{
    DialogueManager.Instance.StartDialogue(myDialogue);
}
```

## Workflow Example

**Creating a Quest Giver NPC:**

1. Create 3 DialogueLine assets:
   - "QuestGiver_Line1": Speaker = "Elder", Text = "Greetings, traveler."
   - "QuestGiver_Line2": Speaker = "Elder", Text = "A great evil threatens our village."
   - "QuestGiver_Line3": Speaker = "Elder", Text = "Will you help us?"

2. Create DialogueContainer "QuestGiver_Intro"
   - Add the 3 lines in order

3. Add Interactable script to your NPC GameObject
   - Assign "QuestGiver_Intro" container
   - Ensure NPC has a collider

4. Click the NPC in-game to see the conversation!

## Technical Highlights

- **Coroutine management** - Proper cleanup prevents text display bugs when skipping
- **State tracking** - `isTyping` flag enables intelligent skip-to-complete behavior
- **Null safety** - Checks prevent errors when coroutines are interrupted
- **Designer-friendly workflow** - No coding required to create dialogue content
- **Reusable assets** - DialogueLines can be shared across multiple containers

## Customization Options

### Adjusting Type Speed
- Lower values = faster typing (0.01 = very fast)
- Higher values = slower typing (0.1 = slow and dramatic)
- Default: 0.05

### Changing Skip Input
Modify the `Update()` method in DialogueManager to change from Space to another key:
```csharp
if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.E)) // Changed to E key
```

## Future Enhancements

- Character portraits/avatars
- Dialogue choices and branching paths
- Audio integration for text sounds
- Triggered events at dialogue end
- Animation triggers
- Localization support
- Save/load dialogue state

## Requirements

- Unity 2020.3 or higher
- TextMeshPro package (included in Unity 2020+)

## Troubleshooting

**Dialogue won't trigger:**
- Ensure object has a Collider2D component (Box Collider 2D, Circle Collider 2D, etc.)
- Check that DialogueContainer is assigned in Interactable script
- Verify DialogueManager exists in scene and is properly configured
- Make sure the Camera has a Physics2D Raycaster if using UI overlay mode

**Text appears instantly:**
- Check Type Speed value in DialogueManager (shouldn't be 0)
- Ensure typingCoroutine is starting properly

---

*Built to demonstrate technical implementation skills while maintaining designer accessibility and workflow efficiency.*
