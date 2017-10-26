SELECT 
Minions.Name,
Minions.Age
FROM Minions
JOIN VillainsMinions ON Minions.MinionId=VillainsMinions.MinionId
WHERE VillainsMinions.VillainId=@villainId
