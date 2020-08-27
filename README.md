# EOPlugin-KCPSTerminal

This is a plugin for [ElectronicObserverExtended](https://eoe.white.ac.cn/) that implements similar interface to [poi-plugin-kcps-terminal](https://github.com/KanaHayama/poi-plugin-kcps-terminal).

## Installation

Download the latest release from the [Releases](https://github.com/LoveJudgement/EOPlugin-KCPSTerminal/releases) page, and extract it to the **root directory** of EO.

i.e., `Nancy.dll` and `Nancy.Hosting.Self.dll` should be put in the same directory with `ElectronicObserver.exe`, and `KCPSTerminal.dll` should be put into `Plugins` directory.

You may need to tweak EO browser settings (`ハードウェアアクセラレーションを有効にする` and `描画バッファを保持する`) to figure out which combination makes your `/capture` work.

Note that this currently works only for the CEF based EOBrowser implementation. Gecko based implementation is not yet supported.

## Config

We currently support only these options. They can be found in Plugin Settings.

* Port: port to listen.
	* We always listen on local loopback interface only. If you really need to run EO and KCPS on different machines you will need some proxy.
* Token: the token to be shared with KCPS.
* JPEG Compression Level: the compression level to be used for `/capture`. Setting this to 0 will cause it to return PNG. 
* Log Priority: priority of our log entries in EO log.
* Log Response: whether to print all JSON responses to EO log too.
	* Please do not turn this on unless you are debugging. When selectors like `ships` are called, this produces roughly 60KB of logs for every 100 ships.
* Mouse Event Mode: the method to simulate mouse events.
    * `WinAPI`: use Win32 API. This has some weird interference with real mouse pointer and sometimes causes EO to steal focus.
    * `IPC`: use whatever implementation provided by `EOBrowser.exe`. For CEF this uses `MouseClickEvent` and `MouseMoveEvent`.
* Capture Mode: the method to capture the screen.
    * `WinAPI`: use Win32 API. This uses an undocumented behavior and potentially will not work on Windows < 8.1.
    * `IPC`: use whatever implementation provided by `EOBrowser.exe`. This method potentially has more overhead and resource footprint because it needs to encode the bitmap multiple times.

Everything else is currently hardcoded (sorry).

* `/capture` scaling: unsupported, we always return original size.

## What's Implemented and What's not

As of KCPS version 1.2.7.5, all features (probably) should work.

### Implemented

* `/capture`
* `/mouse`
	* `down`, `up` and `move`
* `/refresh`
* `/response`
* `/data`
	* `basic`
	* `fleets`
	* `ships`
	* `equips`
	* `repairs`
	* `constructions`
	* `resources`
	* `sortie` (only the used fields)
		* `escapedPos`: unverified, because this is only used during an event.
		* `currentNode`
	* `battle` (only the used fields)
		* `result.deckHp`: unverified for combined fleet
		* `result.enemyShipId`
		* `result.enemyHp`
		* `result.mapCell`
		* Note that for everything in `result`, our lifecycle is different than Poi. It just... happens to work because KCPS checks this at battle result page only.
	* `miscellaneous`
	    * `combinedFleet`
	    * `combinedFleetType`
	* `landBasedAirCorps`
	* `preSets`
		* EO does not persist this so this is implemented ourselves.
		* Only includes field `api_deck`.

### Unimplemented

Everything else, particularly:

* `/data`
	* `const`: unused
	* `maps`: unused, and EO does not persist `api_get_member/mapinfo` now.

## License

EOPlugin-KCPSTerminal is licensed under [GNU General Public License v3.0](https://github.com/LoveJudgement/EOPlugin-KCPSTerminal/blob/master/LICENSE).

It uses [Nancy](https://github.com/NancyFx/Nancy) which is licensed under [MIT License](https://github.com/NancyFx/Nancy/blob/master/license.txt).

[ElectronicObserverExtended](https://github.com/CAWAS/ElectronicObserverExtended) and its dependencies are licensed under their own respective licenses.