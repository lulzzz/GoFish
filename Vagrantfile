Vagrant.configure("2") do |config|
  config.vm.define "oneServerSetup" do |oneServerSetup|
    oneServerSetup.vm.box = "ubuntu/trusty64"
    oneServerSetup.vm.hostname = "GoFish"

    oneServerSetup.vm.network :forwarded_port, guest: 15672, host: 15672
    oneServerSetup.vm.network :forwarded_port, guest: 5000, host: 8081
    oneServerSetup.vm.network :forwarded_port, guest: 5001, host: 8082

    oneServerSetup.vm.synced_folder ".", "/vagrant"

    oneServerSetup.ssh.shell = "bash -c 'BASH_ENV=/etc/profile exec bash'"
    oneServerSetup.vm.provision "shell", path: "RabbitMQ.sh"

    oneServerSetup.vm.provision "docker"
    oneServerSetup.vm.provision :docker_compose, rebuild: true, run: "always", yml: "/vagrant/docker-compose.yml"
  end
end
