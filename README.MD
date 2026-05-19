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
6. Crear API Token Jira en https://id.atlassian.com/manage-profile/security/api-tokens 
    - Pulsa Crear token de API
    - Nombra el token
    - Guarda el token debido a que no se volvera a mostrar
7. Guardar las issue key de jira buscando en este link: https://TUORG.atlassian.net/rest/api/3/issue/SDP-1/transitions (Recuerda cambiar SDP por la sigla de los id de etiqueta):
    - Columna HECHO: 31
    - Columna FALLIDO: 2
8. Guardar los secrets en Github -> Repository -> Settings -> Secrets and variables -> Actions -> New repository secrets:
    - Name: JIRA_API_TOKEN / Secret: token generado Jira
    - Name: JIRA_EMAIL / Secret: tuemail@empresa.com
    - Name: JIRA_BASE_URL / Secret: https://tuempresa.atlassian.net
9. Crear reportes online en pagina https://TUUSUARIO.github.io/TUREPO/
    1. Crear rama gh-pages en git bash:
        - git checkout --orphan gh-pages
        - git reset --hard
        - echo "# Allure Reports" > README.md
            git add README.md
            git commit -m "Initial gh-pages commit"
        - git push origin gh-pages
        - git checkout main
    2. Activar GitHub Pages en Repository -> Settings -> Pages:
        - Source: Deploy from a branch
        - Branch: gh-pages
        - Folder: / (root)
    3. Agregar deploy al workflow
    4. Dar permisos al workflow en Repository -> Settings -> Actions -> General:
        - Busca Workflow permissions
        - Selecciona Read and write permissions
    5. Hacer push:
        - git add .
            git commit -m "Add Allure GitHub Pages deployment"
            git push origin main