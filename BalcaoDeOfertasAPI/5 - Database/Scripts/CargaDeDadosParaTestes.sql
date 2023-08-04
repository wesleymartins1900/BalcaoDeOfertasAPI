USE [BalcaoDb];
GO

-- Carga de dados para Usuario
INSERT INTO Usuario (Id, Nome) VALUES 
('EF4E48BD-4325-4A0B-A5BA-509B6FD43594', 'Joao'),  -- Usuario 1
('40F382D1-2215-4785-A4F2-E2591385925D', 'Maria'),  -- Usuario 2
('9B13797D-04F0-4B46-9DF8-19E9DF27885D', 'José'),  -- Usuario 3
('DC6BDE16-B76B-4025-871B-B870BCAA00CF', 'Paulo'),  -- Usuario 4
('5E945E7B-E22F-4BDF-8B9D-8820062C6F54', 'Joaquin');  -- Usuario 5
-- SELECT * FROM Usuario

-- Carga de dados para Carteira
INSERT INTO Carteira (Id, UsuarioId, Nome) VALUES 
('7A94F61B-2EDD-44F6-A3D2-F289D145A82E', 'EF4E48BD-4325-4A0B-A5BA-509B6FD43594', 'Carteira do Joao 1'), -- Carteira 1 do Usuario 1
('E5F85E43-66D2-4C17-BAD7-974510EDAE23', 'EF4E48BD-4325-4A0B-A5BA-509B6FD43594', 'Carteira do Joao 2'), -- Carteira 2 do Usuario 1
('CBA09E1D-0D57-4001-8B90-5CD0CAC7A738', 'EF4E48BD-4325-4A0B-A5BA-509B6FD43594', 'Carteira do Joao 3'), -- Carteira 3 do Usuario 1
('542B05EF-1701-475E-80ED-856D96BC4114', '40F382D1-2215-4785-A4F2-E2591385925D', 'Carteira da Maria 1'), -- Carteira 1 do Usuario 2
('70AF9382-D30C-4AC5-8914-F967770091A6', '40F382D1-2215-4785-A4F2-E2591385925D', 'Carteira da Maria 2 '), -- Carteira 2 do Usuario 2
('ED86477C-11B4-4AA1-B4E5-03EA745045CC', '9B13797D-04F0-4B46-9DF8-19E9DF27885D', 'Carteira do Jose'), -- Carteira 1 do Usuario 3
('353A795D-25B6-4129-91FF-57E113AED3D0', 'DC6BDE16-B76B-4025-871B-B870BCAA00CF', 'Carteira do Paulo'); -- Carteira 1 do Usuario 4
-- SELECT * FROM Carteira

-- Carga de dados para Moeda
INSERT INTO Moeda (Id, Nome, QuantidadeTotal, ValorReal, CarteiraId)  VALUES 
('42156407-5A38-45E4-80C7-AFBDA83AFBA9', 'Moeda 1', 10, 10.0, '7A94F61B-2EDD-44F6-A3D2-F289D145A82E'), -- Carteira 1 do Usuario 1
('EB6072E9-267C-4F40-982D-43A43E7BE219', 'Moeda 2', 20, 10.0, 'E5F85E43-66D2-4C17-BAD7-974510EDAE23'), -- Carteira 2 do Usuario 1
('A5177645-D3A5-4C49-9C21-A99F64F13351', 'Moeda 3', 30, 10.0, '542B05EF-1701-475E-80ED-856D96BC4114'), -- Carteira 1 do Usuario 2
('1D40EBDD-4B61-45C2-B6B6-9225C8F083EC', 'Moeda 4', 40, 10.0, '70AF9382-D30C-4AC5-8914-F967770091A6'), -- Carteira 2 do Usuario 2
('4C570CA3-B289-4697-AA51-DCF00A11DCDF', 'Moeda 5', 50, 10.0, 'ED86477C-11B4-4AA1-B4E5-03EA745045CC'), -- Carteira 1 do Usuario 3
('9B84435E-C407-4330-BFC9-E56B0821DE83', 'Moeda 6', 60, 10.0, '353A795D-25B6-4129-91FF-57E113AED3D0'); -- Carteira 1 do Usuario 4
-- SELECT * FROM Moeda

SELECT  u.Nome AS Usuario,
		c.Nome AS Carteira,
		m.Nome AS Moeda
FROM	Moeda	 m 	
JOIN	Carteira c ON (m.CarteiraId = c.Id)
JOIN	Usuario  u ON (c.UsuarioId = u.Id)
ORDER BY
		Usuario,
		Carteira,
		Moeda