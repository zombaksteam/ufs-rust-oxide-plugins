using Oxide.Core.Configuration;
using Oxide.Core.Plugins;
using Oxide.Core;
using Oxide.Game.Rust;
using Rust;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System;
using UnityEngine;

namespace Oxide.Plugins {
	[Info("A000Fast000", "Zombak", "0.0.3")]
	[Description("[UFS] Ukraine Fast Server")]
	class A000Fast000 : RustPlugin {
		const string c_ColorMsgError = "red";
		const string c_ColorMsgSuccess = "#C4FF00";
		const string c_ApiWebSiteUrl = "http://ufs.co.ua";
		const string c_ApiWebSiteToken = "secret";

		// Return server IP
		string GetServerIp() {
			return ConVar.Server.ip.ToString();
		}

		// Return server Port
		string GetServerPort() {
			return ConVar.Server.port.ToString();
		}

		// Return server IP:Port
		string GetServerAddress() {
			return GetServerIp() + ":" + GetServerPort();
		}

		// Return server ID
		string GetServerId() {
			string LServerIp = GetServerAddress();
			if(LServerIp == "") {
				return "";
			}
			// TODO: re-think this, replace with auto-generation or ENV
			if(LServerIp == "178.20.158.236:28015") {
				// Server #1
				return "x2x3m3p";
			} else if(LServerIp == "178.20.158.236:28016") {
				// Server #2
				return "x2x3m3b";
			} else if(LServerIp == "178.20.158.236:28017") {
				// Server #3
				return "x2x3sp";
			} else if(LServerIp == "178.20.158.236:28018") {
				// Server #4
				return "x2x3sb";
			} else if(LServerIp == "178.20.158.236:28019") {
				// Server #5
				return "df";
			} else if(LServerIp == "178.20.158.236:28020") {
				// Server #6
				return "x2x3pvep";
			} else if(LServerIp == "178.20.158.236:28021") {
				return "unlimited";
			} else if(LServerIp == "178.20.158.236:28022") {
				// Development server
				// Test for Deah Fight
				return "dev";
			} else {
				// Development server
				return "dev";
			}
		}

		// Return user language by SteamId
		string GetUserLang(string ASteamId) {
			return lang.GetMessage("FastUserLang", this, ASteamId);
		}

		// Return user language by BasePlayer
		string GetUserLang(BasePlayer APlayer) {
			return GetUserLang(APlayer?.UserIDString);
		}

		// User is admin or not
		bool IsUserAdmin(string ASteamId) {
			// TODO: re-think this, replace with auto-generation
			return ASteamId == "76561198078585952";
		}

		// User is admin or not
		bool IsUserAdmin(BasePlayer APlayer) {
			return IsUserAdmin(APlayer.UserIDString);
		}

		// User is moderator or not
		bool IsUserModer(string ASteamId) {
			if(IsUserAdmin(ASteamId)) return true;
			return permission.UserHasGroup(ASteamId, "fmoder");
		}

		// User is moderator or not
		bool IsUserModer(BasePlayer APlayer) {
			return IsUserModer(APlayer.UserIDStrin);
		}

		// User is vip or not
		bool IsUserVIP(string ASteamId) {
			if(IsUserAdmin(ASteamId)) return true;
			if(IsUserModer(ASteamId)) return true;
			return permission.UserHasGroup(ASteamId, "vip");
		}

		// User is vip or not
		bool IsUserVIP(BasePlayer APlayer) {
			return IsUserVIP(APlayer.UserIDString);
		}

		string GetColorMsgError() {
			return c_ColorMsgError;
		}

		string GetColorMsgSuccess() {
			return c_ColorMsgSuccess;
		}

		string GetApiWebSiteUrl() {
			return c_ApiWebSiteUrl;
		}

		string GetApiWebSiteToken() {
			return c_ApiWebSiteToken;
		}

		void Init() {
			// Needs for GetUserLang function
			lang.RegisterMessages(new Dictionary<string, string>{{"FastUserLang", "en"}}, this);

			// Auto group creation
			rust.RunServerCommand($"oxide.group add admin");
			rust.RunServerCommand($"oxide.group add fmoder");
			rust.RunServerCommand($"oxide.group add vip");
			rust.RunServerCommand($"oxide.group add god");
			rust.RunServerCommand($"oxide.group add InSteamGroup");
			Puts($"ServerIP: (" + GetServerAddress() + "), ID: (" + GetServerId() + ")");
		}
	}
}