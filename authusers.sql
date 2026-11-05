-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Nov 05. 11:29
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `authusers`
--
CREATE DATABASE IF NOT EXISTS `authusers` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `authusers`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `roles`
--

CREATE TABLE `roles` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) DEFAULT NULL,
  `RegTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `roles`
--

INSERT INTO `roles` (`Id`, `Name`, `RegTime`) VALUES
('53a991a3-91dc-414e-8a05-96af53b69121', 'Larp Laci', '2026-11-05 11:27:54');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `userrole`
--

CREATE TABLE `userrole` (
  `UserId` char(36) NOT NULL,
  `RoleId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` text DEFAULT NULL,
  `RegTime` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`Id`, `Name`, `Email`, `Password`, `RegTime`) VALUES
('54ed3018-a2c2-4942-822b-4ade7497395c', 'Larp Laci', 'larpolok@gmail.com', 'Luzer1', '2026-11-05 09:37:46'),
('691aceca-85ab-41db-8249-953fc7fe8ecc', 'engi king', 'csiralaci@gmail.com', 'boss2', '2026-11-05 10:40:05');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `userrole`
--
ALTER TABLE `userrole`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `UserId` (`UserId`,`RoleId`),
  ADD KEY `RoleId` (`RoleId`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `userrole`
--
ALTER TABLE `userrole`
  ADD CONSTRAINT `userrole_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `userrole_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
