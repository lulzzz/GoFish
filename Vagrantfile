Vagrant.configure("2") do |config|

  config.vm.define "web" do |web|
    web.vm.box = "ubuntu/trusty64"

    web.vm.hostname = "my-web"

    web.vm.network :"private_network", ip: "192.168.33.10"
    #, virtualbox__intnet: true

    # web.vm.network :forwarded_port, guest: 5000, host: 8081,  auto_correct: true
    # web.vm.network :forwarded_port, guest: 5672, host: 5672,  auto_correct: true

    # web.vm.synced_folder "../multi_test", "/vagrant_data"

    web.vm.provision "docker"
  end

  config.vm.define "mq" do |mq|
    mq.ssh.shell = "bash -c 'BASH_ENV=/etc/profile exec bash'"

    mq.vm.box = "ubuntu/trusty64"

    mq.vm.hostname = "my-rabbit"

    mq.vm.synced_folder "../multi_test", "/vagrant_data"

    mq.vm.network "private_network", ip: "192.168.33.11"
    # mq.vm.network :forwarded_port, guest: 15672, host: 15672, auto_correct: true
    # mq.vm.network :forwarded_port, guest: 4369, host: 4369,  auto_correct: true
    # mq.vm.network :forwarded_port, guest: 5672, host: 5672,  auto_correct: true

    mq.vm.provision "shell", path: "rabbitmq.sh"
  end
end
