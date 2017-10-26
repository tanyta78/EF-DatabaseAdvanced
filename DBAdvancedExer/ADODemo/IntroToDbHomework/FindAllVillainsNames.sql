SELECT 
Villains.Name,
COUNT(MinionId) as [MinionsCount]
FROM Villains
JOIN VillainsMinions ON Villains.VillainId=VillainsMinions.VillainId
GROUP BY Villains.VillainId,Villains.Name
HAVING COUNT(MinionId)>3
ORDER BY MinionsCount DESC
