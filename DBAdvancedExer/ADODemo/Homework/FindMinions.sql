USE MinionsDB

SELECT Name,Age FROM Minions as m
INNER JOIN ViliansMinions as vm
ON vm.MinionId=m.Id
WHERE vm.VilianId=@vilianId
