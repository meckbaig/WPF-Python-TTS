import sys
from whisper import _download, _MODELS
size = str(sys.argv[1])
path = str(sys.argv[2])

_download(_MODELS[size], path, False)