cat >> /etc/apt/sources.list.rabbitmq.list <<EOT
deb http://www.rabbitmq.com/debian/ stable main
EOT

apt-get update
apt-get install -q -y rabbitmq-server

service rabbitmq-server stop
rabbitmq-plugins enable rabbitmq_management
service rabbitmq-server start

rabbitmq-plugins list