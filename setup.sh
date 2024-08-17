#!/bin/bash

# Configurações do MySQL
MYSQL_USER="root"
MYSQL_PASS=""

# Nome do banco de dados a ser criado
DB_NAME="aikotest"

# Comando para criar o banco de dados
CREATE_DB_CMD="CREATE DATABASE IF NOT EXISTS \`$DB_NAME\`;"

# Executa o comando MySQL
mysql -u "$MYSQL_USER" -p"$MYSQL_PASS" -e "$CREATE_DB_CMD"

# Verifica se o comando foi bem-sucedido
if [ $? -eq 0 ]; then
  echo "Banco de dados '$DB_NAME' criado com sucesso!"
else
  echo "Falha ao criar o banco de dados '$DB_NAME'."
fi

cd TheatricalPlayersAPI

dotnet ef database update
