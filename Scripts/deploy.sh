# Need Inno setup and Unity folder in Path
echo "Begin deploy..."
./build_proto.sh
./build_graph.sh
read -p "$*"