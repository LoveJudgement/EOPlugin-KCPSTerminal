# EOPlugin-KCPSTerminal

This is a plugin for [ElectronicObserverExtended](https://eoe.white.ac.cn/) that implements similar interface to [poi-plugin-kcps-terminal](https://github.com/KanaHayama/poi-plugin-kcps-terminal).

## Installation

Download the latest release from the [Releases](https://github.com/LoveJudgement/EOPlugin-KCPSTerminal/releases) page, and extract it to the **root directory** of EO.

i.e., `Nancy.dll` and `Nancy.Hosting.Self.dll` should be put in the same directory with `ElectronicObserver.exe`, and `KCPSTerminal.dll` should be put into `Plugins` directory.

You may need to tweak EO browser settings (`ハードウェアアクセラレーションを有効にする` and `描画バッファを保持する`) to figure out which combination makes your `/capture` work.

Note that this currently works only for the CEF based EOBrowser implementation. Gecko based implementation is not yet supported.

## Config

We currently support only 2 options. They can be found in Plugin Settings.

* Port: port to listen
	* We always listen on local loopback interface only. If you really need to run EO and KCPS on different machines you will need some proxy.
* Log Priority: priority of our log entries in EO log
* Token: the token to be shared with KCPS
* Log Response: whether to print all JSON responses to EO log too
	* Please do not turn this on unless you are debugging. When selectors like `ships` are called, this produces roughly 60KB of logs for every 100 ships.

Everything else is currently hardcoded (sorry).

* /capture related settings (format, compression level, scale etc.): unsupported, we currently hardcode JPEG at 80 (same with the default value of poi-plugin-kcps-terminal).
* /mouse simulation mode: unsupported, we always use Win32 API now.
	* This is because EOBrowser runs in a separate process and ElectronicObserver communicates with it using WCF. We are restricted on what we can do, unless we modify upstream.

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
	* `landBasedAirCorps`
	* `preSets`
		* EO does not persist this so this is implemented ourselves.
		* Only includes field `api_deck`.

### Unimplemented

Everything else, particularly:

* `/data`
	* `const`: unused
	* `resources`: unused
	* `maps`: unused, and EO does not persist `api_get_member/mapinfo` now.

## License

EOPlugin-KCPSTerminal is licensed under [GNU General Public License v3.0](https://github.com/LoveJudgement/EOPlugin-KCPSTerminal/blob/master/LICENSE).

It uses [Nancy](https://github.com/NancyFx/Nancy) which is licensed under [MIT License](https://github.com/NancyFx/Nancy/blob/master/license.txt).

[ElectronicObserverExtended](https://github.com/CAWAS/ElectronicObserverExtended) and its dependencies are licensed under their own respective licenses.