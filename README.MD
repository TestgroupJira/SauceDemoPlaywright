1. Subir repositorio a github, incluir .gitignore
2. Crear token:
    1. Ir a la pagina de github: https://github.com/settings/tokens
    2. Generate new token -> Generate new token (classic)
    3. Note = NombreDelToken
    4. Marcar opciones:
        - repo
        - workflow
    5. Copiar y guardar el token puesto que github no vuelve a mostrarlo.
3. Crear archivo .github/workflows/playwright.yml
4. Este archivo cada vez que se realice la accion push en git, accionara github actions para la ejecucion de las pruebas en un entorno virtual de windows y la conexion a jira via API.
5. Ingresar a Jira y crear un panel tipo kanban:
    1. En el tablero, hacer click en el boton de automatizacion en la parte superior derecha del tablero.
    2. Haz click en "Crear Automatizacion"
    3. En los desencadenadores, selecciona "Transicion de actividad realizada"
    4. Configura un estado de origen y un estado de destino.
    5. Agrega condicion: Condicion de los campos de actividad:
        - Campo: Etiquetas
        - Condicion: No esta vacia
    6. Agrega una accion que:
        - Envie una peticion web (POST) con la URL: https://api.github.com/repos/TU_USUARIO/TU_REPO/dispatches
        - Cuerpo de la solicitud web:
            - Datos personalizados
        - Encabezados Llave / Valor:
            - Authorization / Bearer TU_TOKEN
            - Accept / application/vnd.github+json
            - Content-Type / application/json
    7. Guardar las issue key de jira
