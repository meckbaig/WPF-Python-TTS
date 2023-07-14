import torch, whisper, os, gc


def load_model(size):
    global model
    devices = torch.device("cuda:0" if torch.cuda.is_available() else "cpu") 
    model = whisper.load_model(size, device=devices)

def load_audio(audioPath, l):
    global result
    options = {}
    if (l != ""):
        options.update({"language": l})
    options.update({"task": "transcribe"})
    result = model.transcribe(audioPath, **options)
    lang = language()
    write_txt("lang.txt", lang)
    text_string = text()
    write_txt("text.txt", text_string)

def language():
    return result["language"]

def text():
    return result["text"]

def write_txt(path, string):
    if (os.path.exists(path)):
        with open(path, "w", encoding='utf8') as f:
            f.write(f"{string}")
    else:
        with open(path, "x", encoding='utf8') as f:
            f.write(f"{string}")

def clear_model():
    del model.encoder
    del model.decoder
    torch.cuda.empty_cache()
    gc.collect()
    

#load_model('tiny')
#load_audio("audio.mp3", "")
#clear_model()





#result = model.transcribe("E:\\User\\Downloads\\MyPythonProject\\audio.mp3", fp16=False)
#print(result["text"])








#
#import whisper, os
#
#audioPath = 'audio.mp3'
#
#
#model = whisper.load_model("small")
#audio = whisper.load_audio(audioPath)
#
#mel = whisper.log_mel_spectrogram(audio).to(model.device)
#_, probs = model.detect_language(mel)
#print(f"Detected language: {max(probs, key=probs.get)}")
#
#
#
#options = whisper.DecodingOptions()
#result = whisper.decode(model, mel, options)
#
#
#print(result.text)
#







#from tkinter import *
#import ctypes
#
#try: # >= win 8.1
#    ctypes.windll.shcore.SetProcessDpiAwareness(True)
#except: # win 8.0 or less
#    ctypes.windll.user32.SetProcessDPIAware()
#
#class Syoma:
#    old = 20
#    
#    def Hello(self):
#        return 'Hello'
#
#root = Tk()
#
#root['bg'] = '#Fafafa'
#student = Syoma()
#root.title(student.Hello())
#root.geometry('456x646')
#
#
#
#label = Label(root, text='sdfsdfsdfsd', bg="white", font=10)
#label.pack()
#btn = Button(root, )
#root.mainloop()






