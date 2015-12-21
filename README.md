# SpaceEngineers_GuiModEnabler
A plugin ( supplied through -plugin &lt;name> ), which allows a user to develop their own HUD.

A. Download & Build
	1. Download / Clone
	2. Point project references to the appropiate files in ../SpaceEngineers/Bin64

B. Use
	1. Copy EnableGuiModding.dll to SpaceEngineers/Bin64
	2. Right click on Space Engineers in Steam
		Select Properties
		Select Set Launch Options, copy:
		-plugin EnableGuiModding.dll
		click OK -> Close
	3. Launch game

C. How to make a GUI MOD?
	This is more complicated.  I suggest looking at the source and/or use a decompiler on Keen's *.dll.
	I provided a very simple example, which just enables the toolbar and nothing else.
		Look at CharacterHud
		
	The sky is the limit.

D. How does this work?
	Basically I create an empty Screen right after a game-session starts.  That screen then loads new Huds through the HudManager interface.

E. What could go wrong?
	1. VAC might get you. So far I have no been affected by VAC and code inspection indicates no one else should either.
	Loading method uses a real game feature, so no 'hacking' is happening.
	
	2. Multiplayer will required your server-mates to download any HUD mods, but if you don't use the plugin they will have no effect.
		Still working how figuring out how to resolve this since I don't want to force other people to use the same HUD.  That would defeat the purpose.
		
	3. I've noticed some weird behavior will the toolbar due to the way its handled on a per-session basis.  My apologies if your settings get wiped.
	
F. Why?
	I wanted a better gui / system to control power.  In order to do that I needed to make a new Power tab, which meant modding the GUI.

G. Where To?
	I would like to clean-up the too-permissive Ilchecker permissions and expand my MyGuiGateway class.
	Hopefully, if I can clean-up / create an API Keen will make the necessary permissions changes and this plugin will become obsolete.
	Maybe start putting out some GUI guide information, since this is a fairly dark area for modding.