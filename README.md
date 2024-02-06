# Form_TMDb
El objetivo de esta prueba es crear un servicio REST que:

- Muestre información sobre una película indicando su título
- Muestre una lista de películas similares

Para esto, consumir la API de [The Movie Database](https://developers.themoviedb.org/3/getting-started/introduction) (TMD) (necesario registrarse):

La información a devolver es la siguiente:

- Título
- Título original
- Puntuación media
- Fecha de estreno
- Sinopsis / Descripción
- Lista (máximo 5) de películas de temática similar (Dar tematica). NOTA: tendrás que utilizar otra operación para recuperarlas. Mostrarla en este formato (Título de película y entre paréntesis el año de estreno), por ejemplo:
	- Título película 1 (1969)
   	- Título película 2 (1998)

* Si para una búsqueda por título la API de TMD devuelve más de un resultado, devolver los resultados sobre el primero de ellos.
