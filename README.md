# BloomFilter
Bloom filter to check for words in a dictionary.

# Output
Bloom filter stats:
hash count: 18
bit size: 9000000
set size: 340000

type 'q' to quit from program
type 'a' to add dictionary to Bloom filter
type 'c' to check if a word exists in the Bloom filter
type 'w' to test if all words in the dictionary exist in the Bloom filter
type 'f' to test false-positive % returned by Bloom filter
type 'p' to test sanity of Bloom filter map by printing it as Binary

a
Done adding words to bloom filter.
c
Enter word:
Aparajit
Aparajit does not exist in bloom filter.
c
Enter word:
diamond
diamond probably exists in bloom filter.
w
Success! All words belong to dictionary.
f
False positive words:
Arimhlzee
ohnBloeelrp
nnisiaikB
'rBcuhsayar
rrueaihurCg
loiaGnl
rnlaMyo
eesntPanrmo'
annTziaan
asairV
blisiaonm'
oilscyassmotb
rscitbkie
myosinch
acsgpmoe
ecrtaien
diosmuctsca
itdinstncos'i
stssesi'rd
etp'esinexd
tafalyl
'zfes
ailssoolclg
iaogrg
gngriogg
odoshxpeau
ipanivishmersyto
lispmeelr
siabitatslnun
ockepk
elmesed
msol'
fnatntoy
ocbdeu
nioboles
iflfcsmiaio
ilganuwobtr
aoetserhv
apabiltieistal
eumaslnplenih
atatsrmaipasmrg
eeiaahncpn
peinpriidse
sntolplaiyotspoi
suolnewsrefp
rmdolpse
rghopnorsn
rdmpetleeyvi
rasnaincrtnioe
tesn'enr
qeurime
scsedee
prrresai
mcpinees
lmitayse
snosypdiez
titl
oistrlieg
teertysepts
cnnedeudsed
ndkeiknu
akyubnltsiam
luusinfpistne
False positive % of Bloom filter: 0.01859054184052266
q
