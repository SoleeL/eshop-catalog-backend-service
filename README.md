# eshop-catalog-backend-service

## Cargar variables de entorno

Para cargar las variables de entorno locales, se da por hecho que se tiene la base de datos de eshop-catalog-database-service levantada.

### Manualmente

Hacerlo de esta manera conlleva tener que ejecutar el script cada vez que se cambien las variables de entorno.

### Automaticamente con Rider

Basta con configurar el arranque:

Pasos para configurar Rider:
Abre las configuraciones de ejecución:

En Rider, abre el menú Run > Edit Configurations....
Selecciona tu configuración de ejecución:

Busca la configuración que estás usando para ejecutar tu servicio (por ejemplo, la configuración de tu proyecto .NET).
Agrega una tarea "Before Launch":

Haz clic en + Add Before Launch Task.
Selecciona Run External Tool.
Configura la tarea para ejecutar tu script:

En la ventana que aparece:
Name: Escribe un nombre descriptivo, como Load .env.
Program: Especifica el ejecutable de bash (por ejemplo, /bin/bash).
Arguments: Introduce el nombre de tu script, por ejemplo:
bash
Copy code
./load_env.sh
Working Directory: Indica la ruta de tu proyecto donde está ubicado el script .sh.
Guarda los cambios:

Haz clic en OK para cerrar la configuración del external tool.
Luego, asegúrate de que esta tarea esté listada en la sección Before Launch de tu configuración de ejecución.
Ejecuta tu servicio:

Ahora, cada vez que inicies tu servicio desde Rider, el script load_env.sh se ejecutará primero y cargará las variables en el entorno.