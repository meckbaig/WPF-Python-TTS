import whisper, os

model = whisper.load_model("small")

def load_audio(audioPath):
    global audio
    audio = audioPath
    if os.path.isfile(audio):
        audio = whisper.load_audio(audio)
    else:
        return 'Not a file'

def language():
    global mel
    mel = whisper.log_mel_spectrogram(audio).to(model.device)
    _, probs = model.detect_language(mel)
    return f"{max(probs, key=probs.get)}"

def return_text():
    options = whisper.DecodingOptions()
    return whisper.decode(model, mel, options)





result = model.transcribe("E:\\User\\Downloads\\MyPythonProject\\audio.mp3", fp16=False)
print(result["text"])








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






