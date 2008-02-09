
EXTRA_DIST = Makefile

# Warning: This is an automatically generated file, do not edit!

srcdir=.
top_srcdir=../

include $(top_srcdir)/Makefile.include
include $(top_srcdir)/config.make

#include $(srcdir)/custom-hooks.make

ifeq ($(CONFIG),DEBUG)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -debug -d:DEBUG
ASSEMBLY = ./bin/Debug/FacturaNet.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Debug


endif

ifeq ($(CONFIG),RELEASE)
ASSEMBLY_COMPILER_COMMAND = gmcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4
ASSEMBLY = ./bin/Release/FacturaNet.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = ./bin/Release


endif


BINARIES = \
	$(FACTURANET)  



FACTURANET = $(BUILD_DIR)/facturanet


FILES = \
	Main.cs \
	AssemblyInfo.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	facturanet.in 

REFERENCES =  \
	System

DLL_REFERENCES = 

CLEANFILES += $(BINARIES) 

#Targets
all: $(ASSEMBLY) $(BINARIES)  $(top_srcdir)/config.make

$(FACTURANET): facturanet
	mkdir -p $(BUILD_DIR)
	cp '$<' '$@'
	chmod u+x '$@'



facturanet: facturanet.in $(top_srcdir)/config.make
	sed -e "s,@prefix@,$(prefix)," -e "s,@PACKAGE@,$(PACKAGE)," < facturanet.in > facturanet


$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(build_resx_resources) : %.resources: %.resx
	resgen2 '$<' '$@'

$(ASSEMBLY) $(ASSEMBLY_MDB): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list)
	make pre-all-local-hook prefix=$(prefix)
	mkdir -p $(dir $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
	make post-all-local-hook prefix=$(prefix)


install-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FACTURANET)
	make pre-install-local-hook prefix=$(prefix)
	mkdir -p $(prefix)/lib/$(PACKAGE)
	cp $(ASSEMBLY) $(ASSEMBLY_MDB) $(prefix)/lib/$(PACKAGE)
	mkdir -p $(prefix)/bin
	test -z '$(FACTURANET)' || cp $(FACTURANET) $(prefix)/bin
	make post-install-local-hook prefix=$(prefix)

uninstall-local: $(ASSEMBLY) $(ASSEMBLY_MDB) $(FACTURANET)
	make pre-uninstall-local-hook prefix=$(prefix)
	rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY))
	test -z '$(ASSEMBLY_MDB)' || rm -f $(prefix)/lib/$(PACKAGE)/$(notdir $(ASSEMBLY_MDB))
	test -z '$(FACTURANET)' || rm -f $(prefix)/bin/$(notdir $(FACTURANET))
	make post-uninstall-local-hook prefix=$(prefix)

install: install-local
uninstall: uninstall-local
clean: clean-local

include $(top_srcdir)/rules.make
