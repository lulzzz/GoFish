Vagrant.configure("2") do |config|

  config.vm.provider "virtualbox" do |v|
    v.memory = 1024
    v.cpus = 2
  end

  config.vm.define "oneServerSetup" do |oneServerSetup|
    oneServerSetup.vm.box = "ubuntu/trusty64"
    oneServerSetup.vm.hostname = "GoFish"

    oneServerSetup.vm.network :forwarded_port, guest: 6379, host: 6379    # REDIS
    oneServerSetup.vm.network :forwarded_port, guest: 5672, host: 5672    # RabbitMQ
    oneServerSetup.vm.network :forwarded_port, guest: 15672, host: 15672  # RabbitMQ Admin
    oneServerSetup.vm.network :forwarded_port, guest: 1113, host: 1113    # EventStore
    oneServerSetup.vm.network :forwarded_port, guest: 2113, host: 2113    # EventStore Admin

    oneServerSetup.vm.network :forwarded_port, guest: 5000, host: 5000 # Identity
    oneServerSetup.vm.network :forwarded_port, guest: 5001, host: 5001 # Advert
    oneServerSetup.vm.network :forwarded_port, guest: 5002, host: 5002 # Identity
    oneServerSetup.vm.network :forwarded_port, guest: 5003, host: 5003 # UI.MVC.Advert
    oneServerSetup.vm.network :forwarded_port, guest: 5005, host: 5005 # UI.MVC.Dashboard

    oneServerSetup.vm.synced_folder ".", "/vagrant"

    oneServerSetup.ssh.shell = "bash -c 'BASH_ENV=/etc/profile exec bash'"
    oneServerSetup.vm.provision "shell", path: "RabbitMQ.sh"

    oneServerSetup.vm.provision "docker"
    oneServerSetup.vm.provision :docker_compose, rebuild: true, run: "always", yml: "/vagrant/docker-compose.yml"
  end
end