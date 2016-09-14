Vagrant.configure("2") do |config|
  config.vm.define "oneServerSetup" do |oneServerSetup|
    oneServerSetup.vm.box = "ubuntu/trusty64"
    oneServerSetup.vm.hostname = "GoFish"

    oneServerSetup.vm.network :forwarded_port, guest: 15672, host: 15672, auto_correct: true
    oneServerSetup.vm.network :forwarded_port, guest: 5672, host: 5672,  auto_correct: true
    oneServerSetup.vm.network :forwarded_port, guest: 5000, host: 8081,  auto_correct: true

    oneServerSetup.vm.synced_folder ".", "/vagrant"

    oneServerSetup.ssh.shell = "bash -c 'BASH_ENV=/etc/profile exec bash'"
    oneServerSetup.vm.provision "shell", path: "RabbitMQ.sh"

    oneServerSetup.vm.provision "docker"
  end
end
