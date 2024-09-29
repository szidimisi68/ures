-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Sze 25. 08:37
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
-- Adatbázis: `pizzahot`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `deliveries`
--

CREATE TABLE `deliveries` (
  `id` int(11) NOT NULL,
  `userID` int(11) NOT NULL,
  `deliveryDate` date NOT NULL,
  `location` varchar(255) NOT NULL,
  `phone` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `orders`
--

CREATE TABLE `orders` (
  `deliveryID` int(11) DEFAULT NULL,
  `pizzaID` int(11) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `ingredients`
--

CREATE TABLE `ingredients` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pizzaingredients`
--

CREATE TABLE `pizzaingredients` (
  `pizzaID` int(11) DEFAULT NULL,
  `ingredientID` int(11) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pizzas`
--

CREATE TABLE `pizzas` (
  `id` int(11) NOT NULL,
  `name` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `price` int(32) DEFAULT NULL,
  `img` blob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `password` varchar(255) NOT NULL,
  `name` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `isAdmin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `deliveries`
--
ALTER TABLE `deliveries`
  ADD PRIMARY KEY (`id`),
  ADD KEY `userID` (`userID`);

--
-- A tábla indexei `orders`
--
ALTER TABLE `orders`
  ADD KEY `deliveryID` (`deliveryID`),
  ADD KEY `pizzaID` (`pizzaID`);

--
-- A tábla indexei `ingredients`
--
ALTER TABLE `ingredients`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `pizzaingredients`
--
ALTER TABLE `pizzaingredients`
  ADD KEY `pizzaID` (`pizzaID`),
  ADD KEY `ingredientID` (`ingredientID`);

--
-- A tábla indexei `pizzas`
--
ALTER TABLE `pizzas`
  ADD PRIMARY KEY (`id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `deliveries`
--
ALTER TABLE `deliveries`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `ingredients`
--
ALTER TABLE `ingredients`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `pizzas`
--
ALTER TABLE `pizzas`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `deliveries`
--
ALTER TABLE `deliveries`
  ADD CONSTRAINT `deliveries_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `users` (`id`);

--
-- Megkötések a táblához `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`deliveryID`) REFERENCES `deliveries` (`id`),
  ADD CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`pizzaID`) REFERENCES `pizzas` (`id`);

--
-- Megkötések a táblához `pizzaingredients`
--
ALTER TABLE `pizzaingredients`
  ADD CONSTRAINT `pizzaingredients_ibfk_1` FOREIGN KEY (`pizzaID`) REFERENCES `pizzas` (`id`),
  ADD CONSTRAINT `pizzaingredients_ibfk_2` FOREIGN KEY (`ingredientID`) REFERENCES `ingredients` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
