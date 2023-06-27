

path = r"\\c566s003\Data\Shared\BasedeDatos_Instrumentacion\Sequencias\test.txt"

with open(path, 'r') as f:
    # Read the contents of the file
    contents = f.read()
    # Print the contents of the file
    print(contents)